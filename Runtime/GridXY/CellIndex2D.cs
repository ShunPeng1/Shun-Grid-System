using System;

namespace Shun_Grid_System
{
    public struct CellIndex2D : ICellIndex
    {
        public int X;
        public int Y;

        public CellIndex2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public static bool operator ==(CellIndex2D left, CellIndex2D right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(CellIndex2D left, CellIndex2D right)
        {
            return !left.Equals(right);
        }
        
        public static CellIndex2D operator +(CellIndex2D left, CellIndex2D right)
        {
            return new CellIndex2D(left.X + right.X, left.Y + right.Y);
        }
        
        public static CellIndex2D operator -(CellIndex2D left, CellIndex2D right)
        {
            return new CellIndex2D(left.X - right.X, left.Y - right.Y);
        }
        
        public static CellIndex2D operator *(CellIndex2D left, int right)
        {
            return new CellIndex2D(left.X * right, left.Y * right);
        }
        
        public static CellIndex2D operator /(CellIndex2D left, int right)
        {
            return new CellIndex2D(left.X / right, left.Y / right);
        }
        
        public static CellIndex2D operator %(CellIndex2D left, int right)
        {
            return new CellIndex2D(left.X % right, left.Y % right);
        }
        
        public static CellIndex2D operator -(CellIndex2D left)
        {
            return new CellIndex2D(-left.X, -left.Y);
        }
        
        public static CellIndex2D operator ++(CellIndex2D left)
        {
            return new CellIndex2D(left.X + 1, left.Y + 1);
        }
        
        public static CellIndex2D operator --(CellIndex2D left)
        {
            return new CellIndex2D(left.X - 1, left.Y - 1);
        }
        
        public static CellIndex2D operator +(CellIndex2D left, int right)
        {
            return new CellIndex2D(left.X + right, left.Y + right);
        }
        
        public static CellIndex2D operator -(CellIndex2D left, int right)
        {
            return new CellIndex2D(left.X - right, left.Y - right);
        }

        public bool Equals(CellIndex2D other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is CellIndex2D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public double Accept(IPathFindingDistanceCost pathFindingDistanceCost, ICellIndex toCellIndex)
        {
            return pathFindingDistanceCost.GetDistanceCost(this, toCellIndex);
        }
    }
}