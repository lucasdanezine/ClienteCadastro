using ClienteCadastro.Application.DTOs;
using FluentValidation;

namespace ClienteCadastro.Application.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteDto>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(100);

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos numéricos.");

        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos numéricos.");
    }
}
