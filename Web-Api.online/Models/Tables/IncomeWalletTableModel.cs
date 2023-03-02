﻿using System;

namespace Web_Api.online.Models.Tables
{
    /// <summary>
    /// Кошелёк входящих платежей, адрес создаётся в кошельке ноды и в самом блокчейне.
    /// </summary>
    public class IncomeWalletTableModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CurrencyAcronim { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Address { get; set; }
        public string AddressLabel { get; set; }
    }
}
