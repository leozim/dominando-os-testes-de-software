namespace DominandoTestesDeUnidades;

public class Calculadora
{
    public double Somar(double v1, double v2)
    {
        return v1 + v2;
    }
    
    public double Dividir(double v1, double v2)
    {
        return v2 != 0 ? v1 / v2 : throw new DivideByZeroException();
    }
}