`ThreeChoiceButton` allows users to select one of three possible states (e.g.
"On", "Off", or "Automatic"). Normally, the button is collapsed,
showing only the currently selected state; when expanded, it
shows all three states so the user can change the selection.

### Adding a `ThreeChoiceButton` to your iOS app

```csharp
using ThreeChoice;
using System.Drawing;
...

public override void ViewDidLoad ()
{
  base.ViewDidLoad ();  

  var position = new PointF (10, 10);
  var button = new ThreeChoiceButton (position);

  button.StateChanged += delegate {
    Console.WriteLine ("State changed to: {0}", button.State);
  };

  View.AddSubview (button);
}
```

### Adding a `ThreeChoiceButton` to your Android app

```csharp
using ThreeChoice;
...

protected override void OnCreate (Bundle bundle)
{
  base.OnCreate (bundle);
  
  var button = new ThreeChoiceButton (this);

  button.StateChanged += delegate {
    Console.WriteLine ("State changed to: {0}", button.State);
  };

  AddContentView (button, new ViewGroup.LayoutParams (
    ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
}
```
