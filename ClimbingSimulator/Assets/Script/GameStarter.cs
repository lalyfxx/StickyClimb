using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; 

public class GameStarter : MonoBehaviour
{
    [Header("UI (Interface)")]
    public GameObject ecranControles;    
    public TextMeshProUGUI texteDecompte;

    private bool jeuDemarre = false;
    private bool decompteEnCours = false;

    void Start()
    {
        Time.timeScale = 0f; 
        
        ecranControles.SetActive(true);
        texteDecompte.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!jeuDemarre && !decompteEnCours)
        {
            bool manettePressee = false;
            if (Gamepad.current != null)
            {
                manettePressee = Gamepad.current.buttonSouth.wasPressedThisFrame ||
                                 Gamepad.current.startButton.wasPressedThisFrame || 
                                 Gamepad.current.rightTrigger.wasPressedThisFrame ||
                                 Gamepad.current.leftTrigger.wasPressedThisFrame;   
            }
            
            bool clavierPresse = false;
            if (Keyboard.current != null)
            {
                clavierPresse = Keyboard.current.spaceKey.wasPressedThisFrame || 
                                Keyboard.current.enterKey.wasPressedThisFrame;
            }

            // Dès qu'on appuie, on lance la séquence
            if (manettePressee || clavierPresse)
            {
                StartCoroutine(LancerDecompte());
            }
        }
    }

    IEnumerator LancerDecompte()
    {
        decompteEnCours = true;
        
        ecranControles.SetActive(false);
        texteDecompte.gameObject.SetActive(true);

        texteDecompte.text = "3";
        yield return new WaitForSecondsRealtime(1f); 
        
        texteDecompte.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        
        texteDecompte.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        
        texteDecompte.text = "Stick !";
        
        Time.timeScale = 1f;
        jeuDemarre = true;

        yield return new WaitForSecondsRealtime(1f);
        texteDecompte.gameObject.SetActive(false);
    }
}