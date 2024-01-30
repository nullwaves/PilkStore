from django.contrib.auth.models import User
from rest_framework import filters
from rest_framework.viewsets import ModelViewSet
from pilkapi.models import Location, Pilk
from pilkapi import serializers


class UserViewSet(ModelViewSet):
    queryset = User.objects.all()
    serializer_class = serializers.UserSerializer


class LocationViewSet(ModelViewSet):
    queryset = Location.objects.all()
    serializer_class = serializers.LocationSerializer
    filter_backends = [filters.SearchFilter]
    search_fields = ['name', 'description']


class PilkViewSet(ModelViewSet):
    queryset = Pilk.objects.all()
    serializer_class = serializers.PilkSerializer
    filter_backends = [filters.SearchFilter]
    search_fields = ['name', 'description']
