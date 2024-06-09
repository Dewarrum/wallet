<script lang="ts">
	import { ProgressBar } from '@skeletonlabs/skeleton';
	import { getProfiles } from './queries';

	export let profileId: string | null;

	const profilesQuery = getProfiles();
</script>

{#if $profilesQuery.isLoading}
	<ProgressBar />
{:else if $profilesQuery.data?.items}
	<select class="select" bind:value={profileId}>
		{#each $profilesQuery.data.items as profile}
			<option value={profile.id}>{profile.id}</option>
		{/each}
	</select>
{/if}
