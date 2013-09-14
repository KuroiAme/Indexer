`ThreeChoiceButton` allows users to select one of three possible states (e.g.
"On", "Off", or "Automatic"). Normally, the button is collapsed,
showing only the currently selected state; when expanded, it
shows all three states so the user can change the selection.

## Examples

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

### Usage

To get or set the selected state:

```csharp
ThreeChoiceButtonState state = button.State;
button.State = ThreeChoiceButtonState.Left;
```

To subscribe to state changes:

```csharp
button.StateChanged += (sender, args) => {
  Console.WriteLine ("State changed: {0}", button.State);
};
```

To change labels:

```csharp
button.LeftText = "Maybe";
button.MiddleText = "Now";
button.RightText = "Never";
```

`ThreeChoiceButton` expands to the right by default. To change this:

```csharp
button.ExpandDirection = ThreeChoiceButtonExpandDirection.Left;
```

To change expand/collapse duration:

```csharp
button.ExpandDuration = 1;
button.CollapseDuration = 2;
```

When the user expands a `ThreeChoiceButton` but doesn't make a selection,
it collapses automatically after 2 seconds. To change this delay:

```csharp
button.CollapseDelay = 3;
```

To set the collapsed width:

```csharp
var button = new ThreeChoiceButton (new PointF (10, 10), width: 100);
```

To change label appearance on iOS (you must change font and color types on Android):

```csharp
button.Font = UIFont.FromName ("Helvetica Neue", 15);
button.TextColor = UIColor.Red;
button.ShadowColor = UIColor.Black;
button.ShadowOffset = new SizeF (1, 1);
```

### Adding ThreeChoiceButton to AXML Layouts

```xml
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"/>
  <ThreeChoice.ThreeChoiceButton
    android:id="@+id/autoButton"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content"
    state="Right"
    expandDirection="Left"
    leftText="Maybe"
    middleText="Now"
    rightText="Never" />
</LinearLayout>
```
