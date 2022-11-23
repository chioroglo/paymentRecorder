using Common.Models.Base;

namespace Common.Models;

public class BankModel : BaseModel
{
    public string Name { get; set; }

    public string Code { get; set; }
}