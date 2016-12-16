﻿using Past.Protocol.IO;
using System;
using System.Collections.Generic;

namespace Past.Tools.Ele
{
    public class Elements
    {
        public byte FileVersion { get; set; }
        public uint ElementsCount { get; set; }
        public Dictionary<int, GraphicalElementData> ElementsMap { get; set; }

        public void FromRaw(BigEndianReader raw)
        {
            try
            {
                int header = raw.ReadByte();
                if (header != 69)
                {
                    throw new Exception("Unknown file format");
                }
                FileVersion = raw.ReadByte();
                ElementsCount = raw.ReadUInt();
                ElementsMap = new Dictionary<int, GraphicalElementData>();
                for (int i = 0; i < ElementsCount; i++)
                {
                    int edId = raw.ReadInt();
                    byte edType = raw.ReadByte();
                    GraphicalElementData ed = GraphicalElementFactory.GetGraphicalElementData(edId, edType);
                    ed.FromRaw(raw);
                    ElementsMap[edId] = ed;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
