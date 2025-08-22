using Microsoft.AspNetCore.Components;
using ProjectBalanceLibrary.Models;
using static ProjectBalanceLibrary.Components.Pages.Loans;

namespace ProjectBalanceLibrary.Components.Pages
{
    public class CreateLoanDialogBase : ComponentBase
    {
        [CascadingParameter] public Radzen.DialogService DialogService { get; set; } = default!;
        [Parameter] public List<Book> Books { get; set; } = new();
        [Parameter] public List<Lender> Lenders { get; set; } = new();
        [Parameter] public CreateLoanVm Model { get; set; } = new();

        protected void Submit() => DialogService.Close(Model);
        protected void Close(object? result) => DialogService.Close(result);
    }
}
