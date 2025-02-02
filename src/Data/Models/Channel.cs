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

namespace FundsManager.Data.Models
{
    public class Channel : Entity
    {
        
        public enum ChannelStatus
        {
            Open = 1,
            Closed = 2
        }
        
        public string FundingTx { get; set; }
        public uint FundingTxOutputIndex { get; set; }

        /// <summary>
        /// Final Channel id by LND
        /// </summary>
        public ulong ChanId { get; set; }
        
        /// <summary>
        /// Capacity in SATS
        /// </summary>
        public long SatsAmount { get; set; }

        public string? BtcCloseAddress { get; set; }

        public ChannelStatus Status { get; set; }
        
        /// <summary>
        /// Indicates if this channel was created by NodeGuard
        /// </summary>
        public bool CreatedByNodeGuard { get; set; }
        
        /// <summary>
        /// Bool to indicate if this channel's liquidity should be automatically managed
        /// </summary>
        public bool IsAutomatedLiquidityEnabled { get; set; }

        #region Relationships

        public ICollection<ChannelOperationRequest> ChannelOperationRequests { get; set; }
        
        public ICollection<LiquidityRule> LiquidityRules { get; set; }
        
        public int SourceNodeId { get; set; }
        public Node SourceNode { get; set; }
        
        public int DestinationNodeId { get; set; }
        public Node DestinationNode { get; set; }

        #endregion

    }
}