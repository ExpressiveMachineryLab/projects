var JSConnection =
{
    JSGetGameQuery: function ()
    {
        var params = new URLSearchParams(window.location.search);
		var gameString = params.get("game");

		if (gameString != null) {
			unityInstance.SendMessage('SL Tool', 'SetTextInput', gameString);
			unityInstance.SendMessage('SL Tool', 'ParseSaveDataString');
		}
    },

    JSGetSIDQuery: function ()
    {
        var params = new URLSearchParams(window.location.search);
		var sidString = params.get("sid");

		if (sidString != null) {
			unityInstance.SendMessage('Session Manager', 'SetSessionID', sidString + "");
			console.log("Found SID " + sidString);
		}
    },
};
mergeInto(LibraryManager.library, JSConnection);