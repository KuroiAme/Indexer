`TipOfTheDay` shows a useful tip about using your app on a daily basis.

## Examples

### Showing a tip of the day on iOS

Create a provider for your tips:

```csharp
using TipOfTheDay;
...

public class HealthyTipsProvider : ITipsProvider
{
  static readonly string[] Tips = {
    "Brush your teeth twice daily",
    "Take a five-minute break from your computer every hour or two"
  };

  public string GetTip (int index)
  {
    return Tips[index];
  }

  public int TipsCount {
    get { return Tips.Length; }
  }
}
```

Then add a single line to your `AppDelegate` to show tips on launch:

```csharp
using TipOfTheDay;
...

public partial class AppDelegate : UIApplicationDelegate
{
  UIWindow window;
  ...

  public override bool FinishedLaunching (UIApplication app, NSDictionary options)
  {
    ...
    TipOfTheDayControl<HealthyTipsProvider>.Show (window);
    return true;
  }
}
``` 
Set whether the tip is shown at startup (setting this to `false` will
cause `Show` to do nothing):

```csharp
TipOfTheDayControl.ShowAtStartup = false;
```

### Showing a tip of the day on Android

On Android, your `ITipsProvider` must also implement `IContextInitializable`:

```csharp
using TipOfTheDay;
...

public class HealthyTipsProvider : ITipsProvider, IContextInitializable
{
  static string[] Tips = {
		"Brush your teeth twice daily",
		"Take a five-minute break from your computer every hour or two"
	};

  // This will be called before GetTip and TipsCount are.
  public void Initialize (Context context)
  {
		// Load your tips from disk, network, resource file, etc.
		...
  }

  public string GetTip (int index)
  {
    return Tips[index];
  }

  public int TipsCount {
    get { return Tips.Length; }
  }
}
```

Then call `Activate` from your main activity's `OnCreate` method:

```csharp
using TipOfTheDay;
...

[Activity (MainLauncher = true)]
public class MainActivity : Activity
{

  protected override void OnCreate (Bundle bundle)
  {
    ...
    TipOfTheDayControl<HealthyTipsProvider>.Activate (this);
  }
}
```

To enable or to disable the tip. (a `Context` is needed to save the
setting):

```csharp
bool enabled = TipOfTheDayControl.IsEnabled (context);
TipOfTheDayControl.Enable (context);
TipOfTheDayControl.Disable (context);
```

To force showing the tip from any part of your app:

```csharp
TipOfTheDayControl<HealthyTipsProvider>.ForceActivate (context);
```

### Further Customization

To customize the tip screen, implement `ITipsSettings`:

```csharp
public class HealthyTipsSettings : ITipsSettings
{
  public string QuitButtonText {
    get { return "Namaste"; }
  }

  public string ShowAtStartupText {
    get { return "Keep me healthy"; }
  }

  public string TitleText {
    get { return "Healthy Tip of the Day"; }
  }
}
```

Then show your customized tip of the day with:

```csharp
// On iOS
TipOfTheDayControl<HealthyTipsProvider, HealthyTipsSettings>.Show (window);

// On Android
TipOfTheDayControl<HealthyTipsProvider, HealthyTipsSettings>.Activate (context);
``` 
