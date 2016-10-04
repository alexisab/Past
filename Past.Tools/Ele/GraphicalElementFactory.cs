using Past.Tools.Ele.Subtypes;
using System;

namespace Past.Tools.Ele
{
    public class GraphicalElementFactory
    {
        public static GraphicalElementData GetGraphicalElementData(int elementId, int elementType)
        {
            switch (elementType)
            {
                case (int)GraphicalElementTypes.NORMAL:
                    return new NormalGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.BOUNDING_BOX:
                    return new BoundingBoxGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.ANIMATED:
                    return new AnimatedGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.ENTITY:
                    return new EntityGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.PARTICLES:
                    return new ParticlesGraphicalElementData(elementId, elementType);
                default:
                    throw new Exception($"Unknown graphical element data type {elementType} for element {elementId}");
            }
        }
    }
}
