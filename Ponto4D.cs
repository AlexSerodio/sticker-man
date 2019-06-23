using System;

namespace sticker_man
{
  public class Ponto4D
  {
    
    private double x;
    private double y;
    private double z;
    private readonly double w;

    public Ponto4D(double x = 0.0, double y = 0.0, double z = 0.0, double w = 1.0)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public static Ponto4D operator +(Ponto4D pto1, Ponto4D pto2) => new Ponto4D(pto1.X + pto2.X, pto1.Y + pto2.Y, pto1.Z + pto2.Z);
    public static Ponto4D operator -(Ponto4D pto1, Ponto4D pto2) => new Ponto4D(pto1.X - pto2.X, pto1.Y - pto2.Y, pto1.Z - pto2.Z);
    public static Ponto4D operator *(Ponto4D pto1, Ponto4D pto2) => new Ponto4D(pto1.X * pto2.X, pto1.Y * pto2.Y, pto1.Z * pto2.Z);
    public static Ponto4D operator /(Ponto4D pto1, Ponto4D pto2) => new Ponto4D(pto1.X / pto2.X, pto1.Y / pto2.Y, pto1.Z / pto2.Z);
    
    public static Ponto4D operator +(Ponto4D pto1, double escalar) => new Ponto4D(pto1.X + escalar, pto1.Y + escalar, pto1.Z + escalar);
    public static Ponto4D operator -(Ponto4D pto1, double escalar) => new Ponto4D(pto1.X - escalar, pto1.Y - escalar, pto1.Z - escalar);
    public static Ponto4D operator *(Ponto4D pto1, double escalar) => new Ponto4D(pto1.X * escalar, pto1.Y * escalar, pto1.Z * escalar);
    public static Ponto4D operator /(Ponto4D pto1, double escalar) => new Ponto4D(pto1.X / escalar, pto1.Y / escalar, pto1.Z / escalar);

    public double X { get => x; set => x = value; }
    public double Y { get => y; set => y = value; }
    public double Z { get => z; set => z = value; }
    public double W { get => w; }

    /// <summary>
    /// Inverte todos os valores das coordenadas do ponto
    /// </summary>
    public void InverterSinal()
    {
      x *= -1;
      y *= -1;
      z *= -1;
    }

    public override string ToString() => "{ X: " + X + " | Y: " + Y + " }";
    
    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
        return false;

      Ponto4D ponto = (Ponto4D) obj;
      
      if((ponto.X != this.X) || (ponto.Y != this.Y)|| (ponto.Z != this.Z))
        return false;

      return true;
    }
    
  }
}