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

    [Fact(DisplayName = "Validar Voucher Tipo Valor Inválido")]
    [Trait("Categoria", "Vendas - Voucher")]
    public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
    {
        // Arrange
        var voucher = new Voucher("", null, null, 
            TipoDescontoVoucher.Valor, 0, DateTime.Now.AddDays(-1), false, true);
        // Act
        var result = voucher.ValidarSeAplicavel();
        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(6, result.Errors.Count);
        Assert.Contains(Voucher.VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        Assert.Contains(Voucher.VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        Assert.Contains(Voucher.VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
        Assert.Contains(Voucher.VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));
        Assert.Contains(Voucher.VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        Assert.Contains(Voucher.VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(c => c.ErrorMessage));
    }
}