from django.contrib.auth.models import User
from rest_framework import serializers

from pilkapi.models import Location, Pilk


class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ['url', 'username', 'email', 'groups']


class LocationSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Location
        fields = ['url', 'parent', 'name', 'description', 'image']


class PilkSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Pilk
        fields = ['url', 'location', 'name', 'description', 'image']
