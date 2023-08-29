var EventOrganizer =
{
    GetId: function () { return localStorage.EpOrgId; }
}


function PopulateProfile() {
    var epOrgId = localStorage.EpOrgId;
    SetById('EpIdValue', epOrgId);
    return epOrgId;
}

function SetById(id, text) {
    document.getElementById(id).innerHTML = text;
}

///This is a JS Funtion Calling a C# Method  
function LoadProfile() {
    var id = PopulateProfile();
    DotNet.invokeMethodAsync('EventManager.Web', 'CsharpFromJs', id).then(result => {  /* do anything with result*/  });

}