using System.Collections.Generic;

namespace AbsenceCalculator.Services
{
    public interface ICsvService<in TInput, out TOutput>
    {
        public TOutput ReadCsv(TInput TEntity);
    }
}
