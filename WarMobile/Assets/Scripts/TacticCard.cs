using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TacticCard : MonoBehaviour
{
    [SerializeField] private string tacticName = "";
    [SerializeField] private float AOESize = 5f;
    [SerializeField] private float tacticHeight = 30f;
    [SerializeField] private int resourceCost = 3;
    [SerializeField] private Text resourceText;
    [SerializeField] GameObject tactic;
    [SerializeField] GameObject reticle;
    [SerializeField] private float yOffset = 20f;

    private GameObject spawnedReticle;
    private Vector2 startPos;
    private Vector2 offsetPos;
    private Button tacticButton;
    private bool tacticReadied;
    private bool reticleSpawned;
    private bool cursorIsHeld;
    private float timeHeld;
    private ResourceController resourceController;

    public delegate void OnTacticPurchased(int resourceCost);
    public OnTacticPurchased onTacticPurchased;


    private void Awake()
    {
        spawnedReticle = null;
        tacticButton = GetComponent<Button>();
        tacticButton.onClick.AddListener(delegate {EnableTactic(true); });
        tacticReadied = false;
        reticleSpawned = false;
        cursorIsHeld = false;
        timeHeld = 0.0f;
    }

    private void Start()
    {
        startPos = transform.position;
        offsetPos = new Vector2(transform.position.x, transform.position.y + yOffset);
        resourceText.text = resourceCost.ToString();
        resourceController = FindObjectOfType<ResourceController>();
    }

    private void Update()
    {
        TrackCursorHeldTime();

        if (tacticReadied)
        {
            if (HoldingButton())
            {
                TrackCursorWithReticle();
            }
            else
            {
                // If player clicks on UI after tactic is readied, disable the tactic.
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    SpawnTacticAtCursorLocation();
                }
                else if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() && !cursorIsHeld)
                {
                    EnableTactic(false);
                }
            }

        }
    }

    /// <summary>
    /// Tracks the current cursor location with the reticle.
    /// </summary>
    private void TrackCursorWithReticle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            SpawnReticle();
            MoveCursorTo(hitInfo.point);
        }
    }

    /// <summary>
    /// Checks to see if the player has held the button for long enough.
    /// </summary>
    /// <returns>True if the player has held the button long enough.</returns>
    private bool HoldingButton()
    {
        return timeHeld >= .3f;
    }

    /// <summary>
    /// Tracks the time the button is held.
    /// </summary>
    private void TrackCursorHeldTime()
    {
        if (cursorIsHeld)
        {
            timeHeld += Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawns the tactic at the current cursor location.
    /// </summary>
    private void SpawnTacticAtCursorLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Show reticle if not already shown.
            TrackCursorWithReticle();

            SpawnTactic(hitInfo.point);
        }
    }

    /// <summary>
    /// Spawns the reticle at cursor position.
    /// </summary>
    private void SpawnReticle()
    {
        if (reticleSpawned)
        {
            return;
        }
        reticleSpawned = true;

        float reticleSize = 1 * (.1f * AOESize);
        spawnedReticle = Instantiate(reticle, transform.position, Quaternion.identity);
        // Changes size of reticle based on tactic size.
        spawnedReticle.transform.localScale = new Vector3(reticleSize, 1, reticleSize);
    }

    /// <summary>
    /// Moves the reticle to current cursor position.
    /// </summary>
    /// <param name="cursorPosition"></param>
    private void MoveCursorTo(Vector3 cursorPosition)
    {
        cursorPosition.y = .7f;
        spawnedReticle.transform.position = cursorPosition;
    }

    /// <summary>
    /// Enables this tactic.
    /// </summary>
    /// <param name="enabled"></param>
    private void EnableTactic(bool enabled)
    {
        if (enabled && resourceController.GetResources() >= resourceCost)
        {
            // Moves card up.
            transform.position = offsetPos;
            tacticReadied = true;
        }
        else if (!enabled)
        {
            // Moves card down.
            transform.position = startPos;
            tacticReadied = false;
            if (reticleSpawned)
            {
                // Disable reticle.
                reticleSpawned = false;
                Destroy(spawnedReticle, 2f);
            }
        }
    }

    /// <summary>
    /// Spawns the tactic.
    /// </summary>
    /// <param name="spawnArea">The position to spawn the tactic.</param>
    private void SpawnTactic(Vector3 spawnArea)
    {
        Instantiate(tactic, new Vector3(spawnArea.x, tacticHeight, spawnArea.z), Quaternion.identity);
        transform.Translate(Vector3.down * 20);
        onTacticPurchased?.Invoke(resourceCost);
        EnableTactic(false);
    }

    /// <summary>
    /// Tells the UI that it is currently being held down.
    /// </summary>
    public void CursorHeld()
    {
        cursorIsHeld = true;

        EnableTactic(true);
    }

    /// <summary>
    /// When cursor is released from being held down. 
    /// Spawns tactic, or disables tacticCard given cursor position.
    /// </summary>
    public void CursorReleased()
    {
        cursorIsHeld = false;
        if (HoldingButton() && tacticReadied)
        {
            // If cursor is over UI. Player wishes to cancel tactic.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                EnableTactic(false);
                timeHeld = 0.0f;
                return;
            }

            // Spawn the tactic.
            SpawnTacticAtCursorLocation();
        }

        timeHeld = 0.0f;
    }
}
