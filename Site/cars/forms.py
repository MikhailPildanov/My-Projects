from .models import Car
from django import forms


class CarForm(forms.ModelForm):
    min_year = forms.IntegerField(min_value=0, label='Год от')
    max_year = forms.IntegerField(min_value=0, label='Год до')

    class Meta:
        model = Car
        fields = ['brand', 'model', 'min_year', 'max_year']

        # widgets = {
        #     "title": TextInput(attrs={
        #         'class': 'form-control',
        #         'placeholder': 'Название статьи'
        #     }),
        #     "anons": TextInput(attrs={
        #         'class': 'form-control',
        #         'placeholder': 'Анонс статьи'
        #     }),
        #     "date": widgets.DateTimeInput(attrs={
        #         'class': 'form-control',
        #         'placeholder': 'Дата публикации'
        #     }),
        #     "full_text": widgets.Textarea(attrs={
        #         'class': 'form-control',
        #         'placeholder': 'Текст статьи'
        #     })
        # }