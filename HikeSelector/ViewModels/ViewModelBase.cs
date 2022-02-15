using ReactiveUI;
using Sextant;

namespace HikeSelector.ViewModels
{
    public abstract class ViewModelBase: ReactiveObject, IActivatableViewModel, IViewModel
    {
        public ViewModelBase()
        {
            Activator = new ViewModelActivator();
        }

        public ViewModelActivator Activator { get; }
        public virtual string Id { get; } = nameof(ViewModelBase);
    }
}