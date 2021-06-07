using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using WeDJ.CustomRenderers;
using WeDJ.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLinespaceLabel), typeof(CustomLinespaceLabelRenderer))]

namespace WeDJ.Droid.Renderers
{

#pragma warning disable CS0618 // Type or member is obsolete
    class CustomLinespaceLabelRenderer : LabelRenderer
    {
        protected CustomLinespaceLabel LineSpacingLabel { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (CustomLinespaceLabel)this.Element;
            }

            var lineSpacing = this.LineSpacingLabel.LineSpacing;

            this.Control.SetLineSpacing(65f, (float)lineSpacing);

            Html.FromHtml(this.Control.Text).ToString().Trim();

            this.UpdateLayout();
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}