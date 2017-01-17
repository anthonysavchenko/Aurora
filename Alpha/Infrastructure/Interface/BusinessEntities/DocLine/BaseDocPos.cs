using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.DocLine {
	
	/// <summary>
	/// Базовая позиция документа
	/// </summary>
	public class BaseDocPos : DomainObject, IDocLine {

		private object _doc;
        /// <summary>
        /// Ссылка на документ-владелец позиции.
        /// </summary>
		public object Doc {
				get { Load(); return _doc; 	}
				set { Load(); _doc = value; }
		}
	}
}
