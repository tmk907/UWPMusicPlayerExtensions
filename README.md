# UWPMusicPlayerExtensions

This library helps you create and use extensions for music players.  
This repository also stores information about extensions, which use this library.

## Example

Music player uses extension to get lyrics for currently playing song.

### Music player code 

Package.appxmanifest
```xaml
<uap3:Extension Category="windows.appExtensionHost">
  <uap3:AppExtensionHost>
	<uap3:Name>uwp.music-player.lyrics</uap3:Name>
  </uap3:AppExtensionHost>
</uap3:Extension>
```
```c#
public async Task<LyricsResponse> GetLyrics(string album, string artist, string title, CancellationToken token)
{
	var helper = new ExtensionClientHelper(ExtensionTypes.Lyrics);
	var client = new LyricsExtensionsClient(helper);

	LyricsRequest request = new LyricsRequest()
	{
		Album = album,
		Artist = artist,
		Title = title,
		PreferSynchronized = false,
	};

	return await client.SendRequestAsync(request, extensions, token);
}
```

### Extension code 

Package.appxmanifest
```xaml
<uap:Extension Category="windows.appService" EntryPoint="{BackgroundService.BackgroundLyricsService}">
  <uap:AppService Name="{com.mylyricsextension}" />
</uap:Extension>
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="uwp.music-player.lyrics" Id="{unique id}" Description="{description}" DisplayName="{displayname}" PublicFolder="{publicfolder}">
	<uap3:Properties>
	  <Service>{com.mylyricsextension}</Service>
	</uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

Background task
```c#
private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
{
	var messageDeferral = args.GetDeferral();
	ValueSet message = args.Request.Message;
	MylyricsService service = new MylyricsService();
	ValueSet returnData = await service.GetLyrics(message);
	await args.Request.SendResponseAsync(returnData);
	messageDeferral.Complete();
}
```
```c#
public async Task<ValueSet> GetLyrics(ValueSet message)
{
	ValueSet response = new ValueSet();

	LyricsExtensionService service = new LyricsExtensionService();
	LyricsRequest request = service.GetRequest(message);
	LyricsResponse result = await SearchLyrics(request);
	response = service.PrepareResponse(result);

	return response;
}
```		
