using System;

namespace stick_man
{
  /// <summary>
  /// Classe com funções matemáticas.
  /// </summary>
  public class Matematica {
    
    public static Ponto4D ptoCirculo(double angulo, double raio) {
      Ponto4D pto = new Ponto4D();
      pto.X = (raio * Math.Cos(Math.PI * angulo / 180.0));
      pto.Y = (raio * Math.Sin(Math.PI * angulo / 180.0));
      pto.Z = 0;
      return(pto);
    }

    public static double DistanciaEuclidiana(Ponto4D ponto1, Ponto4D ponto2)
    {
      double distanciaAoQuadrado = (ponto1.X - ponto2.X) * (ponto1.X - ponto2.X) + (ponto1.Y - ponto2.Y) * (ponto1.Y - ponto2.Y);
      return distanciaAoQuadrado;
    }

  }
}