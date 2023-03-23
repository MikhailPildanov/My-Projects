from django.urls import path
from . import views

app_name = 'cars'

urlpatterns = [
    path('', views.index, name='index'),
    path('cars/<int:id>', views.detail, name='detail'),
    path('cars/<int:id>/delete', views.CarDeleteView.as_view(), name='delete'),
    path('rate', views.rate, name='rate'),
    path('create', views.CarCreateView.as_view(), name='create'),
    path('about', views.about, name='about')
]