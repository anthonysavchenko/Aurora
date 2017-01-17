using System;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.DocLine {
	
	public interface IDocLine: IDomainObject 
    {
		/// <summary>
		/// Документ к которому принадлежит позиция.
		/// </summary>
		object Doc { get; set;}
	}
}
