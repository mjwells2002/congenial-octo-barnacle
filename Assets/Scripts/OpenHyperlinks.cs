using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

[RequireComponent(typeof(TMP_Text))]
public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler {
 
    public void OnPointerClick(PointerEventData eventData) {
        TMP_Text pTextMeshPro = GetComponent<TMP_Text>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, eventData.position, null);  // If you are not in a Canvas using Screen Overlay, put your camera instead of null
        if (linkIndex != -1) { // was a link clicked?
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
            
			openIt(linkInfo.GetLinkID());
        }
    }
	
	[DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
		#if !UNITY_EDITOR && UNITY_WEBGL
			OpenNewTab(url);
		#else
			Application.OpenURL(url);
		#endif
    }
}