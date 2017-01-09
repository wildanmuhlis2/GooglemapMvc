namespace Jmelosegui.Mvc.GoogleMap.Drawing
{       
    public struct Point
    {   
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public int X
        {
            get; set;
        }
    
        public int Y
        {
            get; set;
        }
        
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return point1.X != point2.X || point1.Y != point2.Y;
        }
                
        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            } 
            Point comp = (Point)obj;
            return comp.X == this.X && comp.Y == this.Y;
        }
        public override int GetHashCode()
        {
            return unchecked (X ^ Y);
        }
    }
}
