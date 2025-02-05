using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IApartmentDataProvider
{
    public UniTask<List<ApartmentData>> GetApartmentsAsync();
}
