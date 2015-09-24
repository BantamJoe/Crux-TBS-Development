using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool selectionMode;

    public GameObject selectorObject;
    public Selector selectionPrefab;
    public GameObject selectionConfirmButton;
    public GameObject turnPanel;
    public GameObject turnPanelPrefab;

    public CharacterPanel characterPanel;

    public ItemPanel itemPanel;
    public InventoryPanel inventoryPanel;

    public GameObject nextTurnButton;
    public GameObject movementPanel;


    public List<Button> skillButtons;
    int skillSelected;

    public Text nameDisplay;
    public Slider healthBar, manaBar, staminaBar;

    BattleManager battleManager;
    CruxGameManager gameManager;
    Player player;

    // Use this for initialization
    void Start()
    {
        gameManager = Database.GameManager();
        battleManager = Database.BattleManager();
        player = gameManager.player;
        gameManager.uiManager = this;
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startBattle()
    {
        turnPanel.gameObject.SetActive(true);
    }

    public void endBattle()
    {
        turnPanel.gameObject.SetActive(false);
        nextTurnButton.gameObject.SetActive(false);
        movementPanel.gameObject.SetActive(true);
    }

    public void setUp(CruxGameManager _gameManager)
    {
        selectionPrefab.setManager(this);
        setSelectionMode(false);
        skillSelected = -1;
        selectionConfirmButton.SetActive(false);
        selectorObject.SetActive(false);
        gameManager = _gameManager;
    }

    public void updateUI()
    {
        player = Database.GameManager().player;
        updateSkillButtons();
        updatePlayerInfo();
        populateTurnPanel();
        updateRelevantPanels();
        if (characterPanel.gameObject.activeInHierarchy)
        {
            characterPanel.updateCharacterPanel();
        }
        if (itemPanel.gameObject.activeInHierarchy)
        {
            itemPanel.updateItemPanel();
        }
        if (inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.updateInventoryList(player.getBattleActor().inventory);
        }
    }

    void updatePlayerInfo()
    {
        nameDisplay.text = player.getBattleActor().name;
        healthBar.maxValue = player.getBattleActor().getStat("maxHealth");
        healthBar.value = player.getBattleActor().getStat("currentHealth");
        manaBar.maxValue = player.getBattleActor().getStat("maxMana");
        manaBar.value = player.getBattleActor().getStat("currentMana");
        staminaBar.maxValue = player.getBattleActor().getStat("maxStamina");
        staminaBar.value = player.getBattleActor().getStat("currentStamina");
    }

    void updateRelevantPanels()
    {
        bool isItMyTurn = battleManager.isItMyTurn();
        nextTurnButton.SetActive(isItMyTurn);
        if (gameManager.getGameState() =="Battle")
            movementPanel.SetActive(isItMyTurn);
        else
            movementPanel.SetActive(true);
    }

    void updateSkillButtons()
    {
        int i = 0;
        player.getBattleActor();
        List<Skill> equippedSkillsList = player.getBattleActor().getEquippedSkills();

        foreach (Button _button in skillButtons)
        {
            _button.gameObject.SetActive(false);
        }

        foreach (Skill equippedSkills in equippedSkillsList)
        {
            //			Debug.Log(skillButtons.ToArray().Length);

            skillButtons.ToArray()[i].image.sprite = equippedSkills.getIcon();
            skillButtons.ToArray()[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void setPlayer(Player _player)
    {
        player = _player;
    }

    public void setSelectionMode(bool _selectionMode)
    {
        selectionMode = _selectionMode;
        if (!selectionMode)
            selectionPrefab.enabled = false;
        else
            selectionPrefab.enabled = true;
        selectorObject.SetActive(_selectionMode);
        selectionConfirmButton.SetActive(_selectionMode);

    }
    public bool getSelectionMode()
    {
        return selectionMode;
    }

    public void skillButtonPressed(int button)
    {
        if (!gameManager.getGameState().Equals("Battle")
            || player.getBattleActor() != battleManager.getActiveActor())
        {

            return;
        }
        if (selectionMode)
        {
            setSelectionMode(false);
            return;
        }
        setSelectionMode(true);
        skillSelected = button;
        updateUI();

    }

    public void selectionConfirmPressed()
    {
        Debug.Log("SelectionConfirmStarted");

        if (battleManager.useSkill(selectionPrefab.getLocation(), player.getBattleActor().getEquippedSkills().ToArray()[skillSelected]))
        {
            setSelectionMode(false);
        }
        Debug.Log("SelectionConfirmPressed");
        updateUI();
    }

    public void populateTurnPanel()
    {
        foreach (Transform child in turnPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (BattleActor _actor in battleManager.turnOrder)
        {
            //Debug.Log("Adding Turnpanel Actor");
            GameObject turnTab = (GameObject)Instantiate(turnPanelPrefab, transform.position, transform.rotation);
            turnTab.GetComponent<TurnPanel>().setUIManager(this);
            turnTab.GetComponent<TurnPanel>().setBattleActor(_actor);
            turnTab.GetComponent<TurnPanel>().updateStats();
            turnTab.transform.SetParent(turnPanel.transform);
        }

    }

    public void createCharacterPanel(BattleActor actor)
    {
        characterPanel.gameObject.SetActive(true);
        characterPanel.createPanel(actor);
    }

    public void nextTurn()
    {
        battleManager.nextTurn();
        updateUI();

    }

    public void useItem(Item item)
    {
        battleManager.useItem(item);
    }

    public BattleActor getPlayerBattleActor()
    {
        return player.getBattleActor();
    }

    public void displayItemPanel(Item item)
    {
        itemPanel.gameObject.SetActive(true);
        itemPanel.setItem(item);
        updateUI();
    }

    public void inventoryToggle()
    {
        if (inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanel.gameObject.SetActive(true);
            updateUI();
        }
    }

    public void characterToggle()
    {
        if (characterPanel.gameObject.activeInHierarchy)
        {
            characterPanel.gameObject.SetActive(false);
        }
        else
        {
            createCharacterPanel(getPlayerBattleActor());
            updateUI();
        }
    }

    public void directionalInput(int input)
    {
        gameManager.directionalInput(input);
    }

    public void toggleBattleButton()
    {
        if (gameManager.getGameState()
            == "Battle")
        {
            gameManager.endBattle();
            Debug.Log("ENDING BATTLE");
        }
        else
        {
            gameManager.StartBattle(getPlayerBattleActor());
            Debug.Log("STARTING BATTLE");
        }
    }

}
