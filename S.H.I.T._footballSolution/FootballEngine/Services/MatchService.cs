﻿using FootballEngine.Domain.Entities;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Services
{
    public class MatchService : IService<Match>
    {
        private readonly MatchRepository _matchRepository = MatchRepository.Instance;

        private static readonly object CreationLock = new object();
        private static MatchService _instance;
        public static MatchService Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MatchService();
                        }
                    }
                }

                return _instance;
            }
        }

        public void Add(Match match)
        {
            _matchRepository.Add(match);
        }

        public void Delete(Guid id)
        {
            _matchRepository.Delete(id);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetAll();
        }

        public Match GetBy(string name)
        {
            return _matchRepository.GetBy(name);
        }

        public Match GetBy(Guid id)
        {
            return _matchRepository.GetBy(id);
        }

        public void Save()
        {
            _matchRepository.Save();
        }
    }
}
