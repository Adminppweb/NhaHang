using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Customers.Services
{
    public class EvaluateService : IEvaluateService
    {
        private const string EvaluateEntityTypeId = "Evaluate";

        private readonly IRepository<Evaluate> _evaluateRepository;
        private readonly IEntityService _entityService;

        public EvaluateService(IRepository<Evaluate> evaluateRepository, IEntityService entityService)
        {
            _evaluateRepository = evaluateRepository;
            _entityService = entityService;
        }

        public void Create(Evaluate evaluate)
        {
            using (var transaction = _evaluateRepository.BeginTransaction())
            {
                evaluate.Slug = _entityService.ToSafeSlug(evaluate.Slug, evaluate.Id, EvaluateEntityTypeId);
                _evaluateRepository.Add(evaluate);
                _evaluateRepository.SaveChanges();

                _entityService.Add(evaluate.Name, evaluate.Slug, evaluate.Id, EvaluateEntityTypeId);
                _evaluateRepository.SaveChanges();

                transaction.Commit();
            }
        }

        public void Update(Evaluate evaluate)
        {
            var slug = _entityService.Get(evaluate.Id, EvaluateEntityTypeId);
            evaluate.Slug = _entityService.ToSafeSlug(evaluate.Slug, evaluate.Id, EvaluateEntityTypeId);
            if (slug != null)
            {
                _entityService.Update(evaluate.Name, evaluate.Slug, evaluate.Id, EvaluateEntityTypeId);
            }
            else
            {
                _entityService.Add(evaluate.Name, evaluate.Slug, evaluate.Id, EvaluateEntityTypeId);
            }
            _evaluateRepository.SaveChanges();
        }

        public async Task Delete(Evaluate evaluate)
        {
            evaluate.IsDeleted = true;
            await _entityService.Remove(evaluate.Id, EvaluateEntityTypeId);
            _evaluateRepository.SaveChanges();
        }
    }
}
