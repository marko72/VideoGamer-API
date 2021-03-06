﻿using System;
using System.Collections.Generic;

namespace SharedModels.DTO.Game
{
	public class Game
	{
		public Game()
		{
			Genres = new List<string>();
			Platforms = new List<string>();
		}

		public int Id { get; set; }		
		public string Name { get; set; }
		public string Engine { get; set; }
		public string PublisherName { get; set; }
        public string DeveloperName { get; set; }
        public string AgeLabel { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string GameMode { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Platforms { get; set; }
		public string ImagePath { get; set; }
	}
}
