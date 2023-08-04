using System.Collections;
using UnityEngine;

public class GamePageController : MonoBehaviour
{
    [SerializeField] public GameObject platePrefab;
    [SerializeField] public AudioClip compActivatePlateSound;
    [SerializeField] public AudioClip playerActivatePlateSound;
    [SerializeField] public AudioClip playerActivatePlateErrorSound;
    [SerializeField] public AudioClip tickSound;

    private int playerClickCounter = 0;
    private Plate[] plates;
    private int secBeforeStartGame = 3;
    private int currentIndex = 0;
    private Coroutine timerCoroutine;
    private ArrayList computerPlates = new ArrayList();
    private bool playerCanClick = false;

    private void Start()
    {
        plates = new Plate[]
        {
            new Plate(false, "red"),
            new Plate(false, "green"),
            new Plate(false, "blue"),
            new Plate(false, "yellow")
        };

        StartTimer();
    }

    private void StartTimer()
    {
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (secBeforeStartGame >= 0)
        {
            yield return new WaitForSeconds(1f);
            secBeforeStartGame--;
            if (secBeforeStartGame >= 0)
                TickSound();
        }

        ActivateRandomPlate();
    }

    private void TickSound()
    {
        if (tickSound != null)
            AudioSource.PlayClipAtPoint(tickSound, Vector3.zero);
    }

    private void ActivateRandomPlate()
    {
        playerCanClick = false;
        int randomNumber = Random.Range(0, plates.Length);
        Plate plate = plates[randomNumber];
        computerPlates.Add(plate);
        BlinkAllCompColors();
    }

    private void BlinkAllCompColors()
    {
        if (currentIndex < computerPlates.Count)
        {
            Plate currentPlate = (Plate)computerPlates[currentIndex];
            currentPlate.SetActive(true);
            ComputerPlateActivationSound();

            StartCoroutine(WaitAndDeactivate(currentPlate));
        }
        else
        {
            currentIndex = 0;
            playerCanClick = true;
        }
    }

    private IEnumerator WaitAndDeactivate(Plate plate)
    {
        yield return new WaitForSeconds(0.5f);
        plate.SetActive(false);
        currentIndex++;
        BlinkAllCompColors();
    }

    private void ComputerPlateActivationSound()
    {
        if (compActivatePlateSound != null)
            AudioSource.PlayClipAtPoint(compActivatePlateSound, Vector3.zero);
    }

    private void PlayerPlateActivationSound()
    {
        if (playerActivatePlateSound != null)
            AudioSource.PlayClipAtPoint(playerActivatePlateSound, Vector3.zero);
    }

    private void PlayerPlateActivationSoundError()
    {
        if (playerActivatePlateErrorSound != null)
            AudioSource.PlayClipAtPoint(playerActivatePlateErrorSound, Vector3.zero);
    }

    public void HandlePlateClick(Plate plate)
    {
        if (plate == (Plate)computerPlates[playerClickCounter])
        {
            PlayerPlateActivationSound();
            if (playerClickCounter != computerPlates.Count - 1)
            {
                playerClickCounter++;
            }
            else
            {
                playerClickCounter = 0;
                playerCanClick = false;
                StartCoroutine(ActivateRandomPlateWithDelay());
            }
        }
        else
        {
            PlayerPlateActivationSoundError();
            // Handle error condition (e.g., game over, reset, etc.)
        }
    }

    private IEnumerator ActivateRandomPlateWithDelay()
    {
        yield return new WaitForSeconds(1f);
        ActivateRandomPlate();
    }

    private void OnBackButtonClick()
    {
        // Handle back button click, e.g., navigate back to the main menu.
    }

    private void OnDestroy()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }
}

[System.Serializable]
public class Plate
{
    public bool active;
    public string color;

    public Plate(bool active, string color)
    {
        this.active = active;
        this.color = color;
    }

    public void SetActive(bool value)
    {
        active = value;
    }
}

