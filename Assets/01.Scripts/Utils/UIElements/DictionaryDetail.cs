using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DictionaryDetail
{
    private VisualElement _container;

    private VisualElement _backBtn;

    private Label _nameLabel;
    private VisualElement _imageBox;
    private Label _descLabel;
    private Label _habitatLabel;
    private Label _favoriteLabel;
    private Label _weightLabel;
    private Label _lenghtLabel;
    private Label _donateLabel;

    public DictionaryDetail(VisualElement root, VisualElement container){
        _container = container;
        FindElement(root);
        AddEvent();
    }

    public void SetUp(DictionaryDataUnit data){
        _nameLabel.text = data.Name;
        _imageBox.style.backgroundImage = new StyleBackground(data.Image);
        _descLabel.text = data.Desc;
        
        switch(data.Habitat){
            case OceanType.RichOcean:
                _habitatLabel.text = "풍요의 바다";
            break;
            case OceanType.SouthOcean:
                _habitatLabel.text = "남쪽의 바다";
            break;
            case OceanType.RainyOcean:
                _habitatLabel.text = "비의 바다";
            break;
            case OceanType.ColdOcean:
                _habitatLabel.text = "추위의 바다";
            break;
            case OceanType.SilenceOcean:
                _habitatLabel.text = "고요의 바다";
            break;
        }

        string favoriteValue = "";
        for(int i = 0; i < data.Favorites.Count; i++){
            if(i < data.Favorites.Count - 1)
                favoriteValue += $"{data.Favorites[i]}, ";
            else
                favoriteValue += data.Favorites[i];
        }
        _favoriteLabel.text = favoriteValue;

        _weightLabel.text = data.MaxWeight.ToString();
        _lenghtLabel.text = data.MaxLenght.ToString();

        if(data.IsDotated){
            _donateLabel.text = "박물관에 기증됨";
        }
        else{
            _donateLabel.text = "박물관에 기증되지 않음";
        }
    }

    private void FindElement(VisualElement root){
        _backBtn = root.Q<VisualElement>("back-btn");
        _nameLabel = root.Q<Label>("name-text");
        _imageBox = root.Q<VisualElement>("fish-image");
        _descLabel = root.Q<Label>("detail-text");
        _habitatLabel = root.Q("live-place").Q<Label>("info-text");
        _favoriteLabel = root.Q("like-element").Q<Label>("info-text");

        VisualElement record = root.Q("records");
        _weightLabel = record.Q("max-weight").Q<Label>("info-text");
        _lenghtLabel = record.Q("max-lenght").Q<Label>("info-text");

        _donateLabel = root.Q<Label>("endownment-text");
    }

    private void AddEvent(){
        _backBtn.RegisterCallback<ClickEvent>(e => {
            Show(false);
        });
    }

    public void Show(bool show){
        if(show){
            _container.AddToClassList("detail");
        }
        else{
            _container.RemoveFromClassList("detail");
        }
    }
}
