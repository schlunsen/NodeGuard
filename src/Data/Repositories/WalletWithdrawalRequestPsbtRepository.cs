/*
 * NodeGuard
 * Copyright (C) 2023  Elenpay
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see http://www.gnu.org/licenses/.
 *
 */

﻿using FundsManager.Data.Models;
using FundsManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FundsManager.Data.Repositories
{
    public class WalletWithdrawalRequestPsbtRepository : IWalletWithdrawalRequestPsbtRepository
    {
        private readonly IRepository<WalletWithdrawalRequestPSBT> _repository;
        private readonly ILogger<WalletWithdrawalRequestPsbtRepository> _logger;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public WalletWithdrawalRequestPsbtRepository(IRepository<WalletWithdrawalRequestPSBT> repository,
            ILogger<WalletWithdrawalRequestPsbtRepository> logger,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _repository = repository;
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<WalletWithdrawalRequestPSBT?> GetById(int id)
        {
            await using var applicationDbContext = await _dbContextFactory.CreateDbContextAsync();

            return await applicationDbContext.WalletWithdrawalRequestPSBTs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<WalletWithdrawalRequestPSBT>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string?)> AddAsync(WalletWithdrawalRequestPSBT type)
        {
            await using var applicationDbContext = await _dbContextFactory.CreateDbContextAsync();

            type.SetCreationDatetime();

            //We set the request status to PSBTSignaturesPending
            var request = await
                applicationDbContext.WalletWithdrawalRequests.FirstOrDefaultAsync(x =>
                    x.Id == type.WalletWithdrawalRequestId);
            try
            {
                if (request != null && !type.IsTemplatePSBT )
                {
                    request.Status = WalletWithdrawalRequestStatus.PSBTSignaturesPending;

                    applicationDbContext.Update(request);
                    await applicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro while setting withdrawal request status");
                return (false, null);
            }

            return await _repository.AddAsync(type, applicationDbContext);
        }

        public async Task<(bool, string?)> AddRangeAsync(List<WalletWithdrawalRequestPSBT> type)
        {
            await using var applicationDbContext = await _dbContextFactory.CreateDbContextAsync();

            return await _repository.AddRangeAsync(type, applicationDbContext);
        }

        public (bool, string?) Remove(WalletWithdrawalRequestPSBT type)
        {
            using var applicationDbContext = _dbContextFactory.CreateDbContext();

            return _repository.Remove(type, applicationDbContext);
        }

        public (bool, string?) RemoveRange(List<WalletWithdrawalRequestPSBT> types)
        {
            using var applicationDbContext = _dbContextFactory.CreateDbContext();

            return _repository.RemoveRange(types, applicationDbContext);
        }

        public (bool, string?) Update(WalletWithdrawalRequestPSBT type)
        {
            using var applicationDbContext = _dbContextFactory.CreateDbContext();

            type.SetUpdateDatetime();

            return _repository.Update(type, applicationDbContext);
        }
    }
}