using Past.Protocol.IO;
using System;

namespace Past.Protocol.Messages
{
    public class PurchasableDialogMessage : NetworkMessage
	{
        public bool buyOrSell;
        public int purchasableId;
        public int price;
        public override uint Id
        {
        	get { return 5739; }
        }
        public PurchasableDialogMessage()
        {
        }
        public PurchasableDialogMessage(bool buyOrSell, int purchasableId, int price)
        {
            this.buyOrSell = buyOrSell;
            this.purchasableId = purchasableId;
            this.price = price;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(buyOrSell);
            writer.WriteInt(purchasableId);
            writer.WriteInt(price);
        }
        public override void Deserialize(IDataReader reader)
        {
            buyOrSell = reader.ReadBoolean();
            purchasableId = reader.ReadInt();
            if (purchasableId < 0)
                throw new Exception("Forbidden value on purchasableId = " + purchasableId + ", it doesn't respect the following condition : purchasableId < 0");
            price = reader.ReadInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
		}
	}
}
