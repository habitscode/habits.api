using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habits.Domain.Models;
using Habits.Domain.Repositories;

namespace Habits.Domain.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task AddAsync(Challenge item)
        {
            item.ChallengeId = Guid.NewGuid().ToString();
            await _challengeRepository.AddAsync(item);
        }

        public async Task DeleteAsync(Challenge item)
        {
            await _challengeRepository.DeleteAsync(item);
        }

        public async Task<Challenge> GetItem(String challengeId)
        {
            var result = await _challengeRepository.GetItem(challengeId);
            return result;
        }

        public async Task<List<Challenge>> GetItems(String teamId)
        {
            var result = await _challengeRepository.GetItems(teamId);
            return result;
        }

        public Task UpdateAsync(Challenge item)
        {
            throw new NotImplementedException();
        }
    }
}
