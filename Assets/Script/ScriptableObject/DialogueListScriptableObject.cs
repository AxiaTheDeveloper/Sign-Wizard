using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DialogueListScriptableObject : ScriptableObject
{
    [Header("Dialogue Wrong Choice")]
    [field : TextArea]
    public string dialogue_playerInventoryFull_Chest_ID, dialogue_playerInventoryFull_Chest_EN;
    public string dialogue_playerInventoryFull_Chest
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_playerInventoryFull_Chest_ID : dialogue_playerInventoryFull_Chest_EN;
        }
    }
    [field : TextArea]
    public string dialogue_barangChestHabis_Chest_ID, dialogue_barangChestHabis_Chest_EN;
    public string dialogue_barangChestHabis_Chest
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_barangChestHabis_Chest_ID : dialogue_barangChestHabis_Chest_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakBerhasilJadi_Cauldron_ID, dialogue_tidakBerhasilJadi_Cauldron_EN;
    public string dialogue_tidakBerhasilJadi_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakBerhasilJadi_Cauldron_ID : dialogue_tidakBerhasilJadi_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaIngredientMasuk_Cauldron_ID, dialogue_tidakAdaIngredientMasuk_Cauldron_EN;
    public string dialogue_tidakAdaIngredientMasuk_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaIngredientMasuk_Cauldron_ID : dialogue_tidakAdaIngredientMasuk_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaTempatPotion_Cauldron_ID, dialogue_tidakAdaTempatPotion_Cauldron_EN;
    public string dialogue_tidakAdaTempatPotion_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaTempatPotion_Cauldron_ID : dialogue_tidakAdaTempatPotion_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaTempat_Penumbuk_ID, dialogue_tidakAdaTempat_Penumbuk_EN;
    public string dialogue_tidakAdaTempat_Penumbuk
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaTempat_Penumbuk_ID : dialogue_tidakAdaTempat_Penumbuk_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaResep_CauldronPenumbuk_ID, dialogue_tidakAdaResep_CauldronPenumbuk_EN;
    public string dialogue_tidakAdaResep_CauldronPenumbuk
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaResep_CauldronPenumbuk_ID : dialogue_tidakAdaResep_CauldronPenumbuk_EN;
        }
    }
    [field : TextArea]
    public string dialogue_sudahPenuh_Cauldron_ID, dialogue_sudahPenuh_Cauldron_EN;
    public string dialogue_sudahPenuh_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_sudahPenuh_Cauldron_ID : dialogue_sudahPenuh_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogue_ingredientKurang_Cauldron_ID, dialogue_ingredientKurang_Cauldron_EN;
    public string dialogue_ingredientKurang_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_ingredientKurang_Cauldron_ID : dialogue_ingredientKurang_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogue_bukanBahanPotion_InventoryUI_ID, dialogue_bukanBahanPotion_InventoryUI_EN;
    public string dialogue_bukanBahanPotion_InventoryUI
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_bukanBahanPotion_InventoryUI_ID : dialogue_bukanBahanPotion_InventoryUI_EN;
        }
    }
    [field : TextArea]
    public string dialogue_bukanBahanTumbukan_InventoryUI_ID, dialogue_bukanBahanTumbukan_InventoryUI_EN;
    public string dialogue_bukanBahanTumbukan_InventoryUI
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_bukanBahanTumbukan_InventoryUI_ID : dialogue_bukanBahanTumbukan_InventoryUI_EN;
        }
    }
    [field : TextArea]
    public string dialogue_bukanPotion_InventoryUI_ID, dialogue_bukanPotion_InventoryUI_EN;
    public string dialogue_bukanPotion_InventoryUI
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_bukanPotion_InventoryUI_ID : dialogue_bukanPotion_InventoryUI_EN;
        }
    }
    [field : TextArea]
    public string dialogue_potionTidakSesuaiQuest_SubmitPotion_ID, dialogue_potionTidakSesuaiQuest_SubmitPotion_EN;
    public string dialogue_potionTidakSesuaiQuest_SubmitPotion
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_potionTidakSesuaiQuest_SubmitPotion_ID : dialogue_potionTidakSesuaiQuest_SubmitPotion_EN;
        }
    }
    [field : TextArea]
    public string dialogue_sedangTidakAdaQuest_InteractObject_ID, dialogue_sedangTidakAdaQuest_InteractObject_EN;
    public string dialogue_sedangTidakAdaQuest_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_sedangTidakAdaQuest_InteractObject_ID : dialogue_sedangTidakAdaQuest_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_cekMailboxDulu_InteractObject_ID, dialogue_cekMailboxDulu_InteractObject_EN;
    public string dialogue_cekMailboxDulu_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_cekMailboxDulu_InteractObject_ID : dialogue_cekMailboxDulu_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_cekQuestDulu_InteractObject_ID, dialogue_cekQuestDulu_InteractObject_EN;
    public string dialogue_cekQuestDulu_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_cekQuestDulu_InteractObject_ID : dialogue_cekQuestDulu_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_sudahMenyelesaikanSemuaQuest_InteractObject_ID, dialogue_sudahMenyelesaikanSemuaQuest_InteractObject_EN;
    public string dialogue_sudahMenyelesaikanSemuaQuest_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_sudahMenyelesaikanSemuaQuest_InteractObject_ID : dialogue_sudahMenyelesaikanSemuaQuest_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakBisaPakaiPenumbuk_ID, dialogue_tidakBisaPakaiPenumbuk_EN;
    public string dialogue_tidakBisaPakaiPenumbuk
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakBisaPakaiPenumbuk_ID : dialogue_tidakBisaPakaiPenumbuk_EN;
        }
    }
    [field : TextArea]
    public string dialogue_SelesaikanQuestSekarang_InteractObject_ID, dialogue_SelesaikanQuestSekarang_InteractObject_EN;
    public string dialogue_SelesaikanQuestSekarang_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_SelesaikanQuestSekarang_InteractObject_ID : dialogue_SelesaikanQuestSekarang_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_belumAdaQuestYangDikirimTidur_InteractObject_ID, dialogue_belumAdaQuestYangDikirimTidur_InteractObject_EN;
    public string dialogue_belumAdaQuestYangDikirimTidur_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_belumAdaQuestYangDikirimTidur_InteractObject_ID : dialogue_belumAdaQuestYangDikirimTidur_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaBarangYangDiminta1_InteractObject_ID, dialogue_tidakAdaBarangYangDiminta1_InteractObject_EN;
    public string dialogue_tidakAdaBarangYangDiminta1_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaBarangYangDiminta1_InteractObject_ID : dialogue_tidakAdaBarangYangDiminta1_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakAdaBarangYangDiminta2_InteractObject_ID, dialogue_tidakAdaBarangYangDiminta2_InteractObject_EN;
    public string dialogue_tidakAdaBarangYangDiminta2_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakAdaBarangYangDiminta2_InteractObject_ID : dialogue_tidakAdaBarangYangDiminta2_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_belumMengecekKotakSuratLevel1_InteractObject_ID, dialogue_belumMengecekKotakSuratLevel1_InteractObject_EN;
    public string dialogue_belumMengecekKotakSuratLevel1_InteractObject
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_belumMengecekKotakSuratLevel1_InteractObject_ID : dialogue_belumMengecekKotakSuratLevel1_InteractObject_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement_ID, dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement_EN;
    public string dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement_ID : dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement_EN;
        }
    }
    [field : TextArea]
    public string dialogue_tidakPerluKeKota_GoingToOtherPlace_ID, dialogue_tidakPerluKeKota_GoingToOtherPlace_EN;
    public string dialogue_tidakPerluKeKota_GoingToOtherPlace
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_tidakPerluKeKota_GoingToOtherPlace_ID : dialogue_tidakPerluKeKota_GoingToOtherPlace_EN;
        }
    }
    [field : TextArea]
    public string dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace_ID, dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace_EN;
    public string dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace_ID : dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace_EN;
        }
    }
    [field : TextArea]
    public string dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace_ID, dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace_EN;
    public string dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace_ID : dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace_EN;
        }
    }
    [field : TextArea]
    public string dialogue_sudahMenyelesaikanPuzzle_PembatasEnding_ID, dialogue_sudahMenyelesaikanPuzzle_PembatasEnding_EN;
    public string dialogue_sudahMenyelesaikanPuzzle_PembatasEnding
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogue_sudahMenyelesaikanPuzzle_PembatasEnding_ID : dialogue_sudahMenyelesaikanPuzzle_PembatasEnding_EN;
        }
    }
    [Header("Dialogue Tutorial")]
    [field : TextArea]
    public string dialogueTutorial_StartTutorial_ID, dialogueTutorial_StartTutorial_EN;
    public string dialogueTutorial_StartTutorial
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_StartTutorial_ID : dialogueTutorial_StartTutorial_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_Cauldron_ID, dialogueTutorial_Cauldron_EN;
    public string dialogueTutorial_Cauldron
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_Cauldron_ID : dialogueTutorial_Cauldron_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_Chest_ID, dialogueTutorial_Chest_EN;
    public string dialogueTutorial_Chest
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_Chest_ID : dialogueTutorial_Chest_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_Dictionary_ID, dialogueTutorial_Dictionary_EN;
    public string dialogueTutorial_Dictionary
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_Dictionary_ID : dialogueTutorial_Dictionary_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_Bed_ID, dialogueTutorial_Bed_EN;
    public string dialogueTutorial_Bed
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_Bed_ID : dialogueTutorial_Bed_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_Tumbuk_ID, dialogueTutorial_Tumbuk_EN;
    public string dialogueTutorial_Tumbuk
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_Tumbuk_ID : dialogueTutorial_Tumbuk_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_SubmitPotion_ID, dialogueTutorial_SubmitPotion_EN;
    public string dialogueTutorial_SubmitPotion
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_SubmitPotion_ID : dialogueTutorial_SubmitPotion_EN;
        }
    }
    [field : TextArea]
    public string dialogueTutorial_StartMaking_ID, dialogueTutorial_StartMaking_EN;
    public string dialogueTutorial_StartMaking
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueTutorial_StartMaking_ID : dialogueTutorial_StartMaking_EN;
        }
    }

    [Header("Dialogue Nerima Gift")]
    [field : TextArea]
    public string dialogueNerimaGift_1_ID, dialogueNerimaGift_1_EN;
    public string dialogueNerimaGift_1
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueNerimaGift_1_ID : dialogueNerimaGift_1_EN;
        }
    }
    [field : TextArea]
    public string dialogueNerimaGift_2_ID, dialogueNerimaGift_2_EN;
    public string dialogueNerimaGift_2
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueNerimaGift_2_ID : dialogueNerimaGift_2_EN;
        }
    }
    [field : TextArea]
    public string dialogueNerimaGift_3_ID, dialogueNerimaGift_3_EN;
    public string dialogueNerimaGift_3
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueNerimaGift_3_ID : dialogueNerimaGift_3_EN;
        }
    }
    [field : TextArea]
    public string dialogueNerimaGift_4_ID, dialogueNerimaGift_4_EN;
    public string dialogueNerimaGift_4
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? dialogueNerimaGift_4_ID : dialogueNerimaGift_4_EN;
        }
    }
}