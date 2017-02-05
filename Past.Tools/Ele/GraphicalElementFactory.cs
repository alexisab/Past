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
                case (int)GraphicalElementTypes.Normal:
                    return new NormalGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.BoundingBox:
                    return new BoundingBoxGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.Animated:
                    return new AnimatedGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.Entity:
                    return new EntityGraphicalElementData(elementId, elementType);
                case (int)GraphicalElementTypes.Particles:
                    return new ParticlesGraphicalElementData(elementId, elementType);
                default:
                    throw new Exception($"Unknown graphical element data type {elementType} for element {elementId}");
            }
        }
    }
}
