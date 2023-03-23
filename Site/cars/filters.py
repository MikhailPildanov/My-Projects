from django.db import models
import django_filters

from cars.models import Car

class CarFilter(django_filters.FilterSet):
    brand = django_filters.CharFilter(field_name='brand', lookup_expr='iexact')
    model = django_filters.CharFilter(field_name='model', lookup_expr='iexact')
    price = django_filters.NumberFilter(field_name='price', lookup_expr='lt', label='Цена')


    class Meta:
        model = Car
        fields = ['brand', 'model', 'year', 'price']