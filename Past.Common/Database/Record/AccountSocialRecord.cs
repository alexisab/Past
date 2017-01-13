using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using Past.Protocol.Enums;

namespace Past.Common.Database.Record
{
    public class AccountSocialRecord
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public SocialContactCategoryEnum Type { get; set; }
        public int TargetAccountId { get; set; }

        public AccountSocialRecord(IDataRecord reader)
        {
            Id = (int)reader["Id"];
            OwnerId = (int)reader["OwnerId"];
            Type = (SocialContactCategoryEnum)(sbyte)reader["Type"];
            TargetAccountId = (int)reader["TargetAccountId"];
        }

        public AccountSocialRecord(int ownerId, SocialContactCategoryEnum type, int targetAccountId)
        {
            OwnerId = ownerId;
            Type = type;
            TargetAccountId = targetAccountId;
        }

        public static List<AccountSocialRecord> ReturnAccountSocialInformations(int ownerId)
        {
            lock (DatabaseManager.Object)
            {
                List<AccountSocialRecord> accountSocial = new List<AccountSocialRecord>();
                MySqlDataReader reader = DatabaseManager.ExecuteQuery($"SELECT * FROM accounts_social WHERE OwnerId = '{ownerId}'");
                while (reader.Read())
                {
                    accountSocial.Add(new AccountSocialRecord(reader));
                }
                reader.Close();
                return accountSocial;
            }
        }
    }
}
