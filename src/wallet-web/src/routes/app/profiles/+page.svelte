<script lang="ts">
	export let data;
	const { profiles } = data;
</script>

{#each profiles as profile}
	<div class="flex flex-row gap-4 p-4">
		<div class="flex flex-1 flex-col gap-2">
			<div class="flex items-center gap-2">
				<div class="flex-1">
					<p class="text-sm font-medium">{profile.id}</p>
					<p class="text-sm text-muted-foreground">{profile.currency}</p>
				</div>
			</div>
			<p class="text-sm text-muted-foreground">{profile.userId}</p>
		</div>
	</div>
{/each}
