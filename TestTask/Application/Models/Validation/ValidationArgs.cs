using System.Reflection;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Models.Validation
{
    internal class ValidationArgs
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationArgs()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var validatorTypes = types.Where(t => typeof(IValidator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            _validators = validatorTypes.Select(t => (IValidator)Activator.CreateInstance(t));

            if (_validators == null || _validators.Count() == 0)
                throw new ArgumentException("Classes from the IValidator interface were not found");
        }
        public ValidationArgs(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public void Validate(Arguments args)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(args);
            }
        }
    }
}
