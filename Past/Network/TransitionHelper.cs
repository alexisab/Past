using Past.Database;
using Past.Utils;
using System;
using System.Collections.Generic;

namespace Past.Network
{
    public class TransitionHelper
    {
        private static Dictionary<string, Account> Tickets = new Dictionary<string, Account>();

        public static void AddPlayerAccount(Account account, string ticket)
        {
            lock (Tickets)
            {
                try
                {
                    if (Tickets.ContainsKey(ticket) || ticket == "")
                        return;
                    Tickets.Add(ticket, account);
                }
                catch (Exception ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
                }
            }
        }

        public static Account ReturnAccount(string ticket)
        {
            lock (Tickets)
            {
                try
                {
                    Account account = null;
                    if (Tickets.ContainsKey(ticket))
                    {
                        account = Tickets[ticket];
                        Tickets.Remove(ticket);
                    }
                    return account;
                }
                catch (Exception ex)
                {
                    ConsoleUtils.Write(ConsoleUtils.type.ERROR, ex.ToString());
                }
            }
            return null;
        }
    }
}
