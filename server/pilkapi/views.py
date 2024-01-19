from django.contrib.auth.models import User
from rest_framework.viewsets import ModelViewSet
from pilkapi.models import Location, Pilk
from pilkapi import serializers


class UserViewSet(ModelViewSet):
    queryset = User.objects.all()
    serializer_class = serializers.UserSerializer


class LocationViewSet(ModelViewSet):
    queryset = Location.objects.all()
    serializer_class = serializers.LocationSerializer


class PilkViewSet(ModelViewSet):
    queryset = Pilk.objects.all()
    serializer_class = serializers.PilkSerializer
