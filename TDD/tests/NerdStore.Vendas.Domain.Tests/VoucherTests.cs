namespace NerdStore.Vendas.Domain.Tests;

public class VoucherTests
{
    // Entidade_Ação_EstadoEsperado
    [Fact(DisplayName = "Validar Voucher Tipo Valor Válido")]
    [Trait("Categoria", "Vendas - Voucher")]
    public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
    {
        // Arrange
        var voucher = new Voucher("PROMO-15-REAIS", 
                        null, 
                        15, 
                        TipoDescontoVoucher.Valor, 
                        1, 
                        DateTime.Now.AddDays(15), 
                        true, 
                        false);
        // Act
        var result = voucher.ValidarSeAplicavel();
        // Assert
        Assert.True(result.IsValid);
    }
}