
using System.Windows.Forms;

namespace Taumis.Infrastructure.Library.UI {
	/// <summary>
	/// Implements a Workspace which shows the smarparts in MDI forms.
	/// </summary>
	public class MdiWorkspace : WindowWorkspace
	{
		private Form parentMdiForm;

		/// <summary>
		/// Constructor specifying the parent form of the MDI child.
		/// </summary>
		public MdiWorkspace(Form parentForm)
			: base()
		{
			this.parentMdiForm = parentForm;
			this.parentMdiForm.IsMdiContainer = true;
		}

		/// <summary>
		/// Gets the parent MDI form.
		/// </summary>
		public Form ParentMdiForm
		{
			get { return parentMdiForm; }
		}

		/// <summary>
		/// Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
		/// </summary>
		/// <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
		/// <param name="smartPartInfo">The information to use to show the smart part.</param>
		protected override void OnShow (Control smartPart, 
			Microsoft.Practices.CompositeUI.WinForms.WindowSmartPartInfo smartPartInfo)
		{
			Form mdiChild = this.GetOrCreateForm(smartPart);
			mdiChild.MdiParent = parentMdiForm;

			this.SetWindowProperties(mdiChild, smartPartInfo);
			mdiChild.Show();
			this.SetWindowLocation(mdiChild, smartPartInfo);
			mdiChild.BringToFront();
		}
	}
}