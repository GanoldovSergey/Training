PLEASE NOTE:

This library (AutoFixture.AutoEntityFramework) has been superseded by LazyEntityGraph.

LazyEntityGraph is an improvement on this library in the following ways:

- Modular architecture: Not tied to AutoFixture or EntityFramework (although these modules exist)
- Model constraints: Specify relationships (one-to-one, one-to-many, many-to-many) and these constraints will be respected when generating entities
- Entity Framework integration: Automatically generate constraints from an EDMX file or code-first DbContext

Install the following two NuGet packages to begin working with LazyEntityGraph:

- LazyEntityGraph.AutoFixture
- LazyEntityGraph.NHibernate

Then check out the Getting Started guide at https://github.com/alexfoxgill/LazyEntityGraph/wiki