using System;
using System.Linq;
using FizzWare.NBuilder;
using UrlBitlyClone.Core.Context;

namespace UrlBitlyClone.Tests.Infrastructure
{
    public class ObjectMother
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMother"/> class.
        /// </summary>
        public ObjectMother(UrlBitlyCloneContext context)
        {
            this.Context = context;
        }

        public UrlBitlyCloneContext Context { get; }

        /// <summary>
        /// Gets the first entity against the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetFirstEntity<T>() where T : class
        {
            return Context.Set<T>().FirstOrDefault();
        }

        public ObjectMother WithUrls(int size, Func<IListBuilder<UrlShortForm>, IOperable<UrlShortForm>> actions = null) => With(size, null, actions);

        internal ObjectMother With<T>(int size, Func<IListBuilder<T>, IOperable<T>> setupAction = null, Func<IListBuilder<T>, IOperable<T>> overrideAction = null) where T : class
        {
            var objects = Builder<T>.CreateListOfSize(size).All();

            if (setupAction != null)
            {
                objects = setupAction(objects);
            }

            if (overrideAction != null)
            {
                objects = overrideAction(objects);
            }

            foreach (var d in objects.Build())
            {
                Context.Add<T>(d);
            }

            Context.SaveChanges();

            return this;
        }
    }
}
