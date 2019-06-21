﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Domain;
using Domain.Relations;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.GameGenre;

namespace EFServices.Services
{
	public class EFGameGenreService : IGameGenreService
	{
		private readonly VideoGamerDbContext _context;

		public EFGameGenreService(VideoGamerDbContext context)
		{
			_context = context;
		}

		public async Task AddGenreToGame(int gameId, CreateGameGenreDTO dto)
		{
			var game = await _context.Games.FindAsync(gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}


			List<Genre> genres = new List<Genre>();

			foreach (var genre in dto.Genres)
			{
				var genreCheck = _context.Genres.Find(genre);
				if (genreCheck == null)
				{
					throw new EntityNotFoundException("Genre");
				} 
				else
				{
					genres.Add(genreCheck);
				}
			}

			if (dto.Genres.Count != genres.Count)
			{
				throw new Exception("One of genres doesn't exist.");
			}

			List<Domain.Relations.GameGenre> pairs = new List<Domain.Relations.GameGenre>();
			foreach(var genre in dto.Genres)
			{
				pairs.Add(new Domain.Relations.GameGenre
				{
					GameId = gameId,
					GenreId = genre
				});
			}

			await _context.GameGenres.AddRangeAsync(pairs);

			await _context.SaveChangesAsync();
		}


		public async Task RemoveGenreFrom(int gameId, DeleteGameGenreDTO dto)
		{
			var game = await  _context
							.Games
							.Include(u => u.GameGenres)
	 					    .FirstOrDefaultAsync(g => g.Id == gameId);

			if (game == null)
			{
				throw new EntityNotFoundException("Game");
			}


			foreach (int item in dto.Genres)
			{
				var genres = game.GameGenres.FirstOrDefault(g => g.GenreId == item);

				game.GameGenres.Remove(genres);

				await _context.SaveChangesAsync();
			}
		}
	}
}
