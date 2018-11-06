﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace NHibernate.Action
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial interface IAfterTransactionCompletionProcess
	{
		/// <summary>
		/// Perform whatever processing is encapsulated here after completion of the transaction.
		/// </summary>
		/// <param name="success">Did the transaction complete successfully?  True means it did.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		Task ExecuteAfterTransactionCompletionAsync(bool success, CancellationToken cancellationToken);
	}
}
