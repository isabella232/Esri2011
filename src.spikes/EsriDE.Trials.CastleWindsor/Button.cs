using System;

namespace EsriDE.Trials.CastleWindsor
{
	public class Button : AddInButton, IButtonView
	{
		private Builder _builder;

		public Button()
		{
			_builder = new Builder(this);

			IButtonPresenter presenter = _builder.GetButtonPresenter();
			presenter.SetView(this);
		}

		public override void OnClick()
		{
			Clicked();
		}

		public void SetCheckedState(CheckedState checkedState)
		{
			Checked = CheckedState.Checked == checkedState;
		}

		public event Action Clicked = delegate{};
	}
}