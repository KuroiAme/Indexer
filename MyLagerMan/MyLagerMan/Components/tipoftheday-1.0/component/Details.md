`TipOfTheDay` shows a useful tip about using your app on a daily basis.

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

*Screenshot assembled with [PlaceIt](http://placeit.breezi.com/).*
