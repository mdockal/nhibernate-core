﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.Linq
{
	using System.Threading.Tasks;
	[TestFixture]
	public class QueryCacheableTestsAsync : LinqTestCase
	{
		protected override void Configure(Configuration cfg)
		{
			cfg.SetProperty(Environment.UseQueryCache, "true");
			cfg.SetProperty(Environment.GenerateStatistics, "true");
			base.Configure(cfg);
		}

		[Test]
		public async Task QueryIsCacheableAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			var x = await ((from c in db.Customers
					 select c)
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			var x2 = await ((from c in db.Customers
					  select c)
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(1), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task QueryIsCacheable2Async()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			var x = await ((from c in db.Customers
					 select c)
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			var x2 = await ((from c in db.Customers
					  select c).ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(0), "Unexpected cache hit count");
		}

		[Test]
		public async Task QueryIsCacheable3Async()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			var x = await ((from c in db.Customers.WithOptions(o => o.SetCacheable(true))
					 select c).ToListAsync());

			var x2 = await ((from c in db.Customers
					  select c).ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(0), "Unexpected cache hit count");
		}

		[Test]
		public async Task QueryIsCacheableWithRegionAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());
			await (Sfi.EvictQueriesAsync("test"));
			await (Sfi.EvictQueriesAsync("other"));

			var x = await ((from c in db.Customers
					 select c)
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("test"))
				.ToListAsync());

			var x2 = await ((from c in db.Customers
					  select c)
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("test"))
				.ToListAsync());

			var x3 = await ((from c in db.Customers
					  select c)
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("other"))
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(2), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task CacheableBeforeOtherClausesAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			await (db.Customers
				.WithOptions(o => o.SetCacheable(true))
				.Where(c => c.ContactName != c.CompanyName).Take(1).ToListAsync());
			await (db.Customers.Where(c => c.ContactName != c.CompanyName).Take(1).ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(0), "Unexpected cache hit count");
		}

		[Test]
		public async Task CacheableRegionBeforeOtherClausesAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());
			await (Sfi.EvictQueriesAsync("test"));
			await (Sfi.EvictQueriesAsync("other"));

			await (db.Customers
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("test"))
				.Where(c => c.ContactName != c.CompanyName).Take(1)
				.ToListAsync());
			await (db.Customers
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("test"))
				.Where(c => c.ContactName != c.CompanyName).Take(1)
				.ToListAsync());
			await (db.Customers
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("other"))
				.Where(c => c.ContactName != c.CompanyName).Take(1)
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(2), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task CacheableRegionSwitchedAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());
			await (Sfi.EvictQueriesAsync("test"));

			await (db.Customers
				.Where(c => c.ContactName != c.CompanyName).Take(1)
				.WithOptions(o => o.SetCacheable(true).SetCacheRegion("test"))
				.ToListAsync());

			await (db.Customers
				.Where(c => c.ContactName != c.CompanyName).Take(1)
				.WithOptions(o => o.SetCacheRegion("test").SetCacheable(true))
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(1), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task GroupByQueryIsCacheableAsync()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			var c = await (db
				.Customers
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			c = await (db
				.Customers
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.ToListAsync());

			c = await (db
				.Customers
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task GroupByQueryIsCacheable2Async()
		{
			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			var c = await (db
				.Customers
				.WithOptions(o => o.SetCacheable(true))
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.ToListAsync());

			c = await (db
				.Customers
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.ToListAsync());

			c = await (db
				.Customers
				.WithOptions(o => o.SetCacheable(true))
				.GroupBy(x => x.Address.Country)
				.Select(x => x.Key)
				.ToListAsync());

			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(2), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(1), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}

		[Test]
		public async Task CanBeCombinedWithFetchAsync()
		{
			//NH-2587
			//NH-3982 (GH-1372)

			Sfi.Statistics.Clear();
			await (Sfi.EvictQueriesAsync());

			await (db.Customers
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			await (db.Orders
				.WithOptions(o => o.SetCacheable(true))
				.ToListAsync());

			await (db.Customers
			   .WithOptions(o => o.SetCacheable(true))
				.Fetch(x => x.Orders)
				.ToListAsync());

			await (db.Orders
				.WithOptions(o => o.SetCacheable(true))
				.Fetch(x => x.OrderLines)
				.ToListAsync());

			var customer = await (db.Customers
				.WithOptions(o => o.SetCacheable(true))
				.Fetch(x => x.Address)
				.Where(x => x.CustomerId == "VINET")
				.SingleOrDefaultAsync());

			customer = await (db.Customers
				.WithOptions(o => o.SetCacheable(true))
				.Fetch(x => x.Address)
				.Where(x => x.CustomerId == "VINET")
				.SingleOrDefaultAsync());

			Assert.That(NHibernateUtil.IsInitialized(customer.Address), Is.True, "Expected the fetched Address to be initialized");
			Assert.That(Sfi.Statistics.QueryExecutionCount, Is.EqualTo(5), "Unexpected execution count");
			Assert.That(Sfi.Statistics.QueryCachePutCount, Is.EqualTo(5), "Unexpected cache put count");
			Assert.That(Sfi.Statistics.QueryCacheHitCount, Is.EqualTo(1), "Unexpected cache hit count");
		}
	}
}
